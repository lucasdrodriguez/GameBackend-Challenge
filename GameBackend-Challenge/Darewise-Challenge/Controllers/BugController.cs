using AutoMapper;
using Darewise_Challenge.AppDbContext;
using Darewise_Challenge.DTOs;
using GameBackend_Challenge.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IO;
using Darewise_Challenge.Services;

namespace Darewise_Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BugController : ControllerBase
    {
        private readonly ApplicationDbContext appDbContext;
        private readonly ILogger<BugController> logger;
        private readonly IMapper mapper;
        private readonly IFilesManager filesManager;

        public BugController(ApplicationDbContext appDbContext, ILogger<BugController> logger, IMapper mapper, IFilesManager filesManager)
        {
            this.appDbContext = appDbContext;
            this.logger = logger;
            this.mapper = mapper;
            this.filesManager = filesManager;
        }


        [HttpGet]
        [ResponseCache(Duration = 30)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<Bug>>> Get()
        {
            return await appDbContext.Bugs.Include(x => x.Player).ToListAsync();
        }



        [HttpGet("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<ActionResult<Bug>> Get(int id)
        {
            var bug = await appDbContext.Bugs.FirstOrDefaultAsync(x => x.Id == id);

            if (bug == null)
            {
                return NotFound("Bug not exist");
            }
            else
            {
                return bug;
            }

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Post([FromForm] BugCreationDTO newBug)
        {
            try
            {
                var player = await appDbContext.Players.FirstOrDefaultAsync(x => x.Id == newBug.PlayerId);

                if (player == null)
                    return BadRequest("There's not an existant player with that id");

                var bug = mapper.Map<Bug>(newBug);
                bug.Player = player;
                bug.CreationTime = DateTime.Now;

                if (newBug.Attachment != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await newBug.Attachment.CopyToAsync(memoryStream);
                        var content = memoryStream.ToArray();
                        var extension = Path.GetExtension(newBug.Attachment.FileName);
                        bug.Attachment = await filesManager.SaveFile(content, extension, "bugs", newBug.Attachment.ContentType);
                    }
                }

                appDbContext.Add(bug);

                await appDbContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest("An error ocurred");
            }

        }

    }
}
