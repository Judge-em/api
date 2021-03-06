﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Dto;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Storage.Tables;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class SummariesController : ControllerBase
    {
        private readonly ISummaryService _summaryService;

        public SummariesController(ISummaryService summaryService)
        {
            _summaryService = summaryService;
        }

        [HttpGet]
        [Route("{gameId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<Summary> GetSummary(int gameId)
        {
            return await _summaryService.GetByGameId(gameId);
        }

        [HttpPost]
        [Route("{gameId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<Summary> GenerateSummary(int gameId)
        {
            var summary = await _summaryService.GenerateAsync(gameId);
            return summary;
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteSummary(int id)
        {
            await _summaryService.DeleteAsync(id);
            return Ok();
        }

        [HttpGet]
        [Route("user/{userId}")]
        public async Task<List<UserSummaryDto>> GetUserSummaries(int userId)
        {
            var summaries = await _summaryService.GetSummariesByUserId(userId);
            return summaries;
        }
    }
}