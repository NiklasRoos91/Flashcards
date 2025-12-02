using Flashcards.Api.Helpers;
using Flashcards.Application.Commons.OperationResult;
using Flashcards.Application.Features.FlashcardsFeature.Commands.CreateFlashcard;
using Flashcards.Application.Features.FlashcardsFeature.Commands.CreateFlashcardList;
using Flashcards.Application.Features.FlashcardsFeature.DTOs.Requests;
using Flashcards.Application.Features.FlashcardsFeature.DTOs.Responses;
using Flashcards.Application.Features.FlashcardsFeature.Queries.GetFlashcardLists;
using Flashcards.Application.Features.FlashcardsFeature.Queries.GetRandomFlashcard;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Flashcards.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlashcardsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FlashcardsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("get-flashcard-lists")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<OperationResult<IEnumerable<FlashcardListResponseDto>>>> GetFlashcardLists(CancellationToken cancellationToken)
        {
            var userId = UserHelper.GetCurrentUserId(User);

            var query = new GetFlashcardListsQuery(userId);

            var result = await _mediator.Send(query, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("get-random-flashcard/{flashcardListId:guid}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<OperationResult<FlashcardRandomResponseDto>>> GetRandomFlashcard(Guid flashcardListId, CancellationToken cancellationToken)
        {
            var query = new GetRandomFlashcardQuery(flashcardListId);

            var result = await _mediator.Send(query, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("create-flashcard-list")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<CreateFlashcardListResponseDto>> CreateFlashcardList([FromBody] CreateFlashcardListDto dto, CancellationToken cancellationToken)
        {
            var userId = UserHelper.GetCurrentUserId(User);

            var command = new CreateFlashcardListCommand(dto, userId);

            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("create-flashcard")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<CreateFlashcardResponseDto>> CreateFlashcard([FromBody] CreateFlashcardDto dto, CancellationToken cancellationToken)
        {
            var userId = UserHelper.GetCurrentUserId(User);

            var command = new CreateFlashcardCommand(dto, userId);

            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
