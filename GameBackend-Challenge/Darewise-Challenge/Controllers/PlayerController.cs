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
    public class PlayerController : ControllerBase
    {
        private readonly ApplicationDbContext appDbContext;
        private readonly ILogger<PlayerController> logger;
        private readonly IMapper mapper;

        public PlayerController(ApplicationDbContext appDbContext, ILogger<PlayerController> logger, IMapper mapper)
        {
            this.appDbContext = appDbContext;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ResponseCache(Duration = 30)]
        public async Task<ActionResult<List<Player>>> Get()
        {
            return await appDbContext.Players.ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<Player> Get(int id)
        {
            return await appDbContext.Players.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost("CreatePlayer")]
        public async Task<ActionResult<string>> CreatePlayer([FromBody] PlayerCreationDTO newPlayerModel)
        {
            try
            {
                var player = await appDbContext.Players.FirstOrDefaultAsync(x => x.Email == newPlayerModel.Email);

                if (player != null)
                    return BadRequest("That email was already registered");

                var newPlayer = mapper.Map<Player>(newPlayerModel);
                newPlayer.Level = 1;
                newPlayer.AccountType = "Basic";

                appDbContext.Add(newPlayer);

                return Ok("Player Added Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest("player can't be created. Please,try again");
            }
        }
    }
}
