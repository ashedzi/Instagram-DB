DROP TABLE IF EXISTS StoryLikes;
DROP TABLE IF EXISTS DirectMessages;
DROP TABLE IF EXISTS Stories;
DROP TABLE IF EXISTS Likes;
DROP TABLE IF EXISTS Comments;
DROP TABLE IF EXISTS Followers;
DROP TABLE IF EXISTS Posts;
DROP TABLE IF EXISTS Users;

CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(30) NOT NULL,
    FirstName NVARCHAR(25) NOT NULL,
    LastName NVARCHAR(25) NOT NULL,
    FullName AS (FirstName + ' ' + LastName) PERSISTED NOT NULL,
    Email NVARCHAR(50) NOT NULL,
    Bio NVARCHAR(1000),
    ProfilePic NVARCHAR(100),
    Status NVARCHAR(50) NOT NULL,
    PostID VARCHAR(20),
    CommentID INT,
    Followers INT NOT NULL,
    Following INT NOT NULL,
    CONSTRAINT UQ_Users_Username UNIQUE (Username),
    CONSTRAINT UQ_Users_Email UNIQUE (Email)
);
CREATE TABLE Posts (
    PostID VARCHAR(20) PRIMARY KEY,
    Timestamp DATETIME NOT NULL,
    Image NVARCHAR(50) NOT NULL,
    Caption NVARCHAR(350),
    Likes INT NOT NULL,
    Saves INT NOT NULL,
    UserID INT NOT NULL,
    CONSTRAINT FK_Posts_User FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
CREATE TABLE Comments (
    CommentID INT PRIMARY KEY IDENTITY(1,1),
    Timestamp DATETIME NOT NULL,
    Likes INT NOT NULL,
    PostID VARCHAR(20) NOT NULL,
    CommenterUserID INT NOT NULL,
    PosterUserID INT NOT NULL,
	Content NVARCHAR(MAX) NOT NULL DEFAULT(''),
    CONSTRAINT FK_Comments_Post FOREIGN KEY (PostID) REFERENCES Posts(PostID),
    CONSTRAINT FK_Comments_Commenter FOREIGN KEY (CommenterUserID) REFERENCES Users(UserID),
    CONSTRAINT FK_Comments_Poster FOREIGN KEY (PosterUserID) REFERENCES Users(UserID)
);
CREATE TABLE Followers (
    Follow_Id INT IDENTITY(1,1) PRIMARY KEY,
    FollowerUserID INT NOT NULL,
    FollowingUserID INT NOT NULL,
    CONSTRAINT FK_Followers_Follower FOREIGN KEY (FollowerUserID) REFERENCES Users(UserID),
    CONSTRAINT FK_Followers_Following FOREIGN KEY (FollowingUserID) REFERENCES Users(UserID),
    CONSTRAINT UQ_Follow UNIQUE (FollowerUserID, FollowingUserID)  -- optional, prevents duplicates
);

 
CREATE TABLE Likes (
    PostID VARCHAR(20) NOT NULL,
    PosterUserID INT NOT NULL,
    LikerUserID INT NOT NULL,
    CONSTRAINT FK_Likes_Post FOREIGN KEY (PostID) REFERENCES Posts(PostID),
    CONSTRAINT FK_Likes_Poster FOREIGN KEY (PosterUserID) REFERENCES Users(UserID),
    CONSTRAINT FK_Likes_Liker FOREIGN KEY (LikerUserID) REFERENCES Users(UserID),
    CONSTRAINT PK_Likes PRIMARY KEY (PostID, LikerUserID)
);
CREATE TABLE Stories (
    StoryID VARCHAR(20) PRIMARY KEY,
    Poster INT NOT NULL,
    Viewer INT,
    Timestamp DATETIME NOT NULL,
    Views INT,
    Image VARCHAR(255),
    Caption TEXT,
    FOREIGN KEY (Poster) REFERENCES Users(UserID),
    FOREIGN KEY (Viewer) REFERENCES Users(UserID)
);
CREATE TABLE StoryLikes (
    StoryID VARCHAR(20),
    Liker INT,
    Timestamp DATETIME NOT NULL,
    PRIMARY KEY (StoryID, Liker),
    FOREIGN KEY (StoryID) REFERENCES Stories(StoryID),
    FOREIGN KEY (Liker) REFERENCES Users(UserID)
);
CREATE TABLE DirectMessages (
    MessageID INT PRIMARY KEY IDENTITY(1,1),
    SenderUserID INT NOT NULL,
    ReceiverUserID INT NOT NULL,
    Timestamp DATETIME NOT NULL,
    TextContent NVARCHAR(400) NOT NULL,
    CONSTRAINT FK_DirectMessages_Sender FOREIGN KEY (SenderUserID) REFERENCES Users(UserID),
    CONSTRAINT FK_DirectMessages_Receiver FOREIGN KEY (ReceiverUserID) REFERENCES Users(UserID)
);
INSERT INTO Users (Username, FirstName, LastName, Email, Bio, ProfilePic, Status, Followers, Following) VALUES
('cristiano', 'Cristiano', 'Ronaldo', 'cristiano@gmail.com', 'Cristiano Ronaldo is a Portuguese football legend...', 'cr7.png', 'Active', 100000, 50),
('messi', 'Lionel', 'Messi', 'messi@gmail.com', 'Lionel Messi is an Argentine football genius...', 'messi.png', 'Active', 120000, 60),
('serenawilliams', 'Serena', 'Williams', 'serena@gmail.com', 'Serena Williams is a tennis icon...', 'serena.png', 'Busy', 80000, 30),
('that_talkative_user', 'Talkie', 'User', 'talkative@gmail.com', 'Curious and always chatting.', NULL, 'Busy', 300, 500),
('aplasticplant', 'A', 'Plant', 'plant@gmail.com', 'Green and growing.', 'plant.png', 'Busy', 200, 100),
('movie_quotes', 'Movie', 'Quotes', 'quotes@gmail.com', 'Posting iconic quotes.', 'quotes.png', 'Active', 450, 80),
('lifeoftheparty', 'Life', 'Party', 'party@gmail.com', 'Always at the center of fun.', 'party.png', 'Active', 300, 200),
('quietfan', 'Quiet', 'Fan', 'quietfan@gmail.com', 'Lurking quietly.', NULL, 'Busy', 950, 5),
('ghostwriter99', 'Ivy', 'Thorne', 'ivy.thorne@example.com', 'Writing in the shadows. No face, just words.', NULL, 'Active', 1000, 100),
('silentstorm', 'Dorian', 'Vale', 'dorian.vale@example.com', 'Loud thoughts. Quiet presence.', NULL, 'Busy', 950, 120),
('obscura_vision', 'Kael', 'Riven', 'kael.riven@example.com', 'Seeing the unseen since 1995.', NULL, 'Idle', 900, 150),
('echo_nomad', 'Sera', 'Nyx', 'sera.nyx@example.com', 'Drifting through digital silence.', NULL, 'Idle', 850, 200);
INSERT INTO Followers (FollowerUserID, FollowingUserID) VALUES
(2, 1),
(3, 1),
(6, 1),
(1, 2),
(4, 2),
(7, 2),
(2, 5),
(2, 6),
(2, 3),
(7, 9),
(7, 10),
(11, 12);
INSERT INTO Posts (PostID, Timestamp, Image, Caption, Likes, Saves, UserID) VALUES
('468JD1592038', '2025-07-18 08:00:00', 'cr1.jpg', 'Training hard', 300, 30, 1),
('823TR6428197', '2025-07-20 08:10:00', 'messi1.jpg', 'Vamos!', 500, 45, 2),
('759WK4271036', '2025-07-17 10:30:00', 'serena1.jpg', 'Practice mode', 200, 20, 3),
('134ZN6584920', '2022-03-15 08:00:00', 'talk1.jpg', 'Coffee chat', 90, 12, 4),
('967QP2318475', '2025-07-10 11:00:00', 'plant1.jpg', 'Nature power', 80, 8, 5),
('580MV9743162', '2025-07-10 09:00:00', 'quote1.jpg', '¡°I¡¯ll be back.¡±', 100, 10, 6),
('291FX8356074', '2025-07-21 08:30:00', 'party1.jpg', 'Let¡¯s go!', 120, 15, 7),
('999XY1234567', '2025-07-01 09:00:00', 'ivy_post.jpg', 'Finally posting!', 10, 1, 9),
('888YX7654321', '2025-07-02 10:00:00', 'vale_post.jpg', 'Hello world!', 15, 2, 10);

INSERT INTO Likes (PostID, PosterUserID, LikerUserID) VALUES
('823TR6428197', 2, 1),  -- cristiano likes messi
('468JD1592038', 1, 2),  -- messi likes cristiano
('967QP2318475', 5, 3),  -- serena likes plant
('967QP2318475', 5, 6),  -- movie_quotes likes plant
('967QP2318475', 5, 8),  -- quietfan likes plant
('759WK4271036', 3, 4),  -- talkative likes serena
('967QP2318475', 5, 4),  -- talkative likes plant
('967QP2318475', 5, 1);  -- cristiano likes plant

INSERT INTO Comments (Timestamp, Likes, PostID, CommenterUserID, PosterUserID, Content) VALUES
('2025-07-20 09:00:00', 15, '823TR6428197', 1, 2, 'Amazing shot, bro!'),  -- cristiano on messi's post
('2025-07-21 07:00:00', 20, '468JD1592038', 2, 1, 'Great hustle! Always inspiring'),  -- messi on cristiano's post
('2025-07-19 09:00:00', 10, '759WK4271036', 4, 3, 'Wow! This is so cool'),  -- talkative on serena's post
('2025-07-19 12:00:00', 5, '967QP2318475', 3, 5, 'Love the vibe of this pic'),   -- serena on plant's post
('2025-07-20 14:00:00', 3, '967QP2318475', 6, 5, 'Nature always wins'),   -- movie_quotes on plant's post
('2025-07-20 15:00:00', 2, '967QP2318475', 8, 5, 'Quiet beauty in this post'),   -- quietfan on plant's post
('2025-07-21 10:00:00', 4, '468JD1592038', 7, 1, 'Let¡¯s party again sometime!'),   -- party on cristiano's post
('2025-07-20 17:00:00', 1, '967QP2318475', 4, 5, 'Can¡¯t get enough of this shot'),   -- talkative on plant's post
('2025-07-21 11:00:00', 3, '967QP2318475', 1, 5, 'Peaceful and powerful');   -- cristiano on plant's post

 
INSERT INTO Stories (StoryID, Poster, Viewer, Timestamp, Views, Image, Caption) VALUES
('S1', 1, 2, '2025-07-20 12:00:00', 500, 'cr_story.jpg', 'Gearing up'),
('S2', 2, 1, '2025-07-20 13:00:00', 550, 'messi_story.jpg', 'Recovery day'),
('S3', 3, 1, '2025-07-19 11:00:00', 400, 'serena_story.jpg', 'Focused'),
('S4', 5, 6, '2025-07-18 11:00:00', 100, 'plant_story.jpg', 'Sunshine life'),
('S5', 6, 8, '2025-07-18 10:30:00', 250, 'quote_story.jpg', 'Quotes for days');
INSERT INTO DirectMessages (SenderUserID, ReceiverUserID, Timestamp, TextContent) VALUES
(4, 1, '2022-03-12 07:30:00', 'Yo CR7! Big fan!'),
(4, 2, '2022-04-01 08:00:00', 'Hey Leo! Let¡¯s talk.'),
(4, 5, '2022-08-01 12:00:00', 'Your plants are cool!'),
(4, 8, '2022-11-01 13:00:00', 'You¡¯re underrated!'),
(4, 6, '2023-01-01 10:00:00', 'Movie quotes are fire'),
(7, 9, '2025-06-15 08:00:00', 'You should start posting!'),
(7, 10, '2025-06-20 08:30:00', 'Waiting for your first post!');
INSERT INTO StoryLikes (StoryID, Liker, Timestamp) VALUES
('S5', 8, '2025-07-18 10:31:00'),
('S5', 3, '2025-07-18 10:35:00'),
('S5', 4, '2025-07-18 10:40:00');

