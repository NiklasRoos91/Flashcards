using Flashcards.Api.Helpers;
using Flashcards.Application.Commons.OperationResult;
using Flashcards.Application.Features.FlashcardlistsFeature.Commands.CreateFlashcardList;
using Flashcards.Application.Features.FlashcardlistsFeature.Commands.DeleteFlashcardList;
using Flashcards.Application.Features.FlashcardlistsFeature.Commands.UpdateFlashcardList;
using Flashcards.Application.Features.FlashcardlistsFeature.DTOs;
using Flashcards.Application.Features.FlashcardlistsFeature.Queries.GetFlashcardLists;
using Flashcards.Application.Features.FlashcardListsFeature.Queries.GetFlashcardListWithFlashcards;
using Flashcards.Application.Features.FlashcardsFeature.DTOs.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Flashcards.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlashcardListsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FlashcardListsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // POST /api/FlashcardLists/create-flashcard-list
        [HttpPost("create-flashcard-list")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<CreateFlashcardListResponseDto>> CreateFlashcardList(
            [FromBody] CreateFlashcardListDto dto,
            CancellationToken cancellationToken)
        {
            var userId = UserHelper.GetCurrentUserId(User);

            var command = new CreateFlashcardListCommand(dto, userId);

            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Data);
        }

        // GET /api/FlashcardLists/get-flashcard-lists
        [HttpGet("get-flashcard-lists")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<OperationResult<IEnumerable<FlashcardListResponseDto>>>> GetFlashcardLists(
            CancellationToken cancellationToken)
        {
            var userId = UserHelper.GetCurrentUserId(User);

            var query = new GetFlashcardListsQuery(userId);

            var result = await _mediator.Send(query, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Data);
        }

        [HttpGet("{flashcardListId:guid}/flashcards")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<OperationResult<List<FlashcardResponseDto>>>> GetFlashcardsForList(
            Guid flashcardListId,
            CancellationToken cancellationToken)
        {
            var userId = UserHelper.GetCurrentUserId(User);

            var query = new GetFlashcardListWithFlashcardsQuery(flashcardListId, userId);
            var result = await _mediator.Send(query, cancellationToken);

            if (result.IsSuccess)
                return Ok(result.Data);

            return NotFound(result.Data);
        }


        // PATCH /api/FlashcardLists/{id}
        [HttpPatch("{id}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<OperationResult<UpdateFlashcardListDto>>> UpdateFlashcardList(
            Guid id, [FromBody] UpdateFlashcardListDto dto, CancellationToken cancellationToken)
        {
            dto.FlashcardListId = id;
            var userId = UserHelper.GetCurrentUserId(User);
            var command = new UpdateFlashcardListCommand(dto, userId);

            var result = await _mediator.Send(command, cancellationToken);

            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Data);
        }

        // DELETE /api/FlashcardLists/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<OperationResult<bool>>> DeleteFlashcardList(Guid id, CancellationToken cancellationToken)
        {
            var userId = UserHelper.GetCurrentUserId(User);
            var command = new DeleteFlashcardListCommand(id, userId);

            var result = await _mediator.Send(command, cancellationToken);

            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Data);
        }
    }
}
