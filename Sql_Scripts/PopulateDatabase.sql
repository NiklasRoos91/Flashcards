-- Insert Users
INSERT INTO [dbo].[Users] (UserId, Username, Email, PasswordHash, FirstName, LastName, Address, PhoneNumber, Role, CreatedAt)
VALUES 
(NEWID(), 'user1', 'user1@example.com', 'hashedpassword1', 'Alice', 'Andersson', 'Street 1', '+46123456701', 0, GETDATE()),
(NEWID(), 'user2', 'user2@example.com', 'hashedpassword2', 'Bob', 'Bengtsson', 'Street 2', '+46123456702', 0, GETDATE()),
(NEWID(), 'user3', 'user3@example.com', 'hashedpassword3', 'Charlie', 'Carlsson', 'Street 3', '+46123456703', 0, GETDATE()),
(NEWID(), 'user4', 'user4@example.com', 'hashedpassword4', 'David', 'Dahl', 'Street 4', '+46123456704', 0, GETDATE()),
(NEWID(), 'user5', 'user5@example.com', 'hashedpassword5', 'Eva', 'Eriksson', 'Street 5', '+46123456705', 0, GETDATE());

-- Insert Tags
INSERT INTO [dbo].[Tags] (TagId, Name)
VALUES
(NEWID(), 'Math'),
(NEWID(), 'Science'),
(NEWID(), 'History'),
(NEWID(), 'Geography'),
(NEWID(), 'English');

-- Insert Flashcard Lists
WITH UserIds AS (
    SELECT UserId, Username FROM [dbo].[Users]
)
INSERT INTO [dbo].[FlashcardLists] (FlashcardListId, Title, CreatedAt, UserId)
SELECT NEWID(), 'List 1 for ' + Username, GETDATE(), UserId FROM UserIds
UNION ALL
SELECT NEWID(), 'List 2 for ' + Username, GETDATE(), UserId FROM UserIds;


-- Insert Flashcards
-- Example: 5–15 flashcards per list
WITH ListIds AS (
    SELECT FlashcardListId, Title FROM [dbo].[FlashcardLists]
)
INSERT INTO [dbo].[Flashcards] (FlashcardId, Question, Answer, CreatedAt, FlashcardListId)
SELECT NEWID(), CONCAT('Question 1 for ', Title), CONCAT('Answer 1 for ', Title), GETDATE(), FlashcardListId FROM ListIds
UNION ALL
SELECT NEWID(), CONCAT('Question 2 for ', Title), CONCAT('Answer 2 for ', Title), GETDATE(), FlashcardListId FROM ListIds
UNION ALL
SELECT NEWID(), CONCAT('Question 3 for ', Title), CONCAT('Answer 3 for ', Title), GETDATE(), FlashcardListId FROM ListIds
UNION ALL
SELECT NEWID(), CONCAT('Question 4 for ', Title), CONCAT('Answer 4 for ', Title), GETDATE(), FlashcardListId FROM ListIds
UNION ALL
SELECT NEWID(), CONCAT('Question 5 for ', Title), CONCAT('Answer 5 for ', Title), GETDATE(), FlashcardListId FROM ListIds;

-- Insert FlashcardTags
-- Assign 1–3 tags per flashcard randomly
WITH CardIds AS (
    SELECT FlashcardId FROM [dbo].[Flashcards]
),
TagIds AS (
    SELECT TagId FROM [dbo].[Tags]
)
INSERT INTO [dbo].[FlashcardTags] (FlashcardId, TagId)
SELECT c.FlashcardId, t.TagId
FROM CardIds c
CROSS JOIN TagIds t
WHERE ABS(CAST(NEWID() AS BINARY(6)) % 3) + 1 >= 1;  -- random 1–3 tags
