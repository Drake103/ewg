using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using EWG.Frontend.Security;
using EWG.Infrastructure.Dal.Repositories;
using EWG.Infrastructure.Services.Replays;
using EWG.Shared.Dto;
using EWG.Web.Attributes;
using Ionic.Zip;

namespace EWG.Web.Controllers
{
    public class ReplaysController : Controller
    {
        private readonly IReplayService _replayService;
        private readonly IReplayRepository _replayRepository;

        public ReplaysController(IReplayService replayService, IReplayRepository replayRepository)
        {
            _replayService = replayService;
            _replayRepository = replayRepository;
        }

        //
        // GET: /Replays/List
        public JsonResult Details(int replayId)
        {
            if (replayId <= 0)
            {
                return NotFound();
            }

            var dto = _replayRepository.GetReplayCard(replayId);

            if (dto == null)
            {
                return NotFound();
            }

            return Json(dto, JsonRequestBehavior.AllowGet);
        }

        private JsonResult NotFound()
        {
            return Json(new {success = false}, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Replays/List
        public JsonResult List(int startIndex, int pageSize, string searchText)
        {
            var pagingInfo = new PagingInfo(startIndex, pageSize);

            var dtos = _replayService.GetReplays(pagingInfo, searchText);
            return Json(dtos, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Replays/List
        public JsonResult GetByPlayerUser(int playerId, int startIndex, int pageSize)
        {
            var pagingInfo = new PagingInfo(startIndex, pageSize);

            var dtos = _replayService.GetReplaysByPlayerUser(playerId, pagingInfo);
            return Json(dtos, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Replays/GetCount
        public JsonResult GetCount(string searchText)
        {
            return Json(_replayService.GetReplaysCount(searchText), JsonRequestBehavior.AllowGet);
        }

        //
        // POST: /Replays/Upload
        [HttpPost]
        public JsonResult Upload(HttpPostedFileBase file)
        {
            try
            {
                var filename = Path.GetFileName(file.FileName);

                if (filename == null) throw new NotSupportedException();

                var randomFilename = Path.GetRandomFileName();
                string title;
                if (_replayService.IsAlreadyUploaded(file.InputStream, out title))
                {
                    return Json(new
                    {
                        success = false,
                        message = string.Format("Replay is already uploaded with title - '{0}'.", title)
                    });
                }

                var replay = _replayService.SaveReplay(file.InputStream, randomFilename);

                var hash = replay.FileHash;

                var path = Path.Combine(Server.MapPath("~/App_Data/replays"), randomFilename);
                file.SaveAs(path);

                return new JsonResult
                {
                    Data = new
                    {
                        replayId = replay.Id,
                        success = true,
                        token = hash
                    }
                };
            }
            catch (Exception ex)
            {
                return new JsonResult
                {
                    Data = new
                    {
                        success = false,
                        message = string.Format("Could not upload the replay - {0}", ex.Message)
                    }
                };
            }
            
        }

        [HttpPost]
        public JsonResult SetTitle(int replayId, Guid token, string newTitle)
        {
            var replay = _replayRepository.GetByHash(token);
            if (replay == null) return Json(new {});

            replay.Title = newTitle;
            _replayRepository.Save(replay);

            return Json(new {success = true});
        }

        public FileResult GetReplayFile(int replayId)
        {
            var replay = _replayRepository.GetById(replayId);
            var path = Path.Combine(Server.MapPath("~/App_Data/replays"), replay.Link);

            byte[] fileBytes = System.IO.File.ReadAllBytes(path);

            replay.DownloadsCounter++;
            _replayRepository.Save(replay);

            using (var zipStream = new MemoryStream())
            {
                using (var zip = new ZipFile())
                {
                    zip.AddEntry(replay.Title + ".wargamerpl2", fileBytes);
                    zip.Save(zipStream);
                }
                
                return File(zipStream.ToArray(), System.Net.Mime.MediaTypeNames.Application.Zip, replay.Title + ".zip");
            }
        }
    }
}
