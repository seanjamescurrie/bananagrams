INSERT INTO bt_anagram_type (title, max_attempts, time_allowed)
VALUES 
	('Daily', 5, 0),
	('Face Off', 3, 30)
;

INSERT INTO brain_teaser (title, description)
VALUES 
	('Anagram', 'Unscramble a set of letters to reveal the hidden word'),
	('Hangman', 'Guess the letters before the man is complete')
;