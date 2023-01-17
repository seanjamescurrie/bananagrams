INSERT INTO users (date_created, email_adress, first_name, last_name, user_name)
VALUES 
	(Current_Timestamp, 'sean.currie@unosquare.com', 'Sean', 'Currie', 'seancurrie'),
	(Current_Timestamp, 'sean.currie+david@unosquare.com', 'David', 'Currie', 'davidcurrie')
;

INSERT INTO games (title, date_created)
VALUES
	('Sean Vs David', current_timestamp),
	('Daily 12/12/2023', current_timestamp)
;

INSERT INTO game_users (game_id, user_id)
VALUES
	(1, 1),
	(2, 1),
	(1, 2)
;

INSERT INTO	words (title, description, image_location)
VALUES 
	('BANANA', 'I am a banana', 'http://tropicalfruitandveg.com/images/bananauk2.jpg'),
	('PASSION FRUIT', 'I am a passion fruit', 'http://tropicalfruitandveg.com/images/passionyel.jpg'),
	('LYCHEE', 'I am a lychee', 'http://tropicalfruitandveg.com/images/lychee2.jpg'),
	('PINEAPPLE', 'I am a pineapple', 'https://tropicalfruitandveg.com/images/pineapple2.jpg')
;

INSERT INTO	game_anagrams (anagram_word, date_created, order_sequence, game_id, word_id, game_anagram_type_id)
VALUES 
	('NANABA', current_timestamp, 1, 1, 1, 1),
	('NSPSAOI RIUTF', current_timestamp, 2, 1, 2, 1),
	('EHLECY', CURRENT_TIMESTAMP, 3, 1, 3, 1),
	('PALPPEINE', current_timestamp, 1, 2, 4, 2)
;

INSERT INTO game_user_game_anagrams (attempts, date_played, date_solved, game_anagram_id, game_user_id)
VALUES 
	(2, current_timestamp, current_timestamp + INTERVAL '1 hour', 1, 1),
	(3, current_timestamp, current_timestamp + INTERVAL '1 hour', 1, 2),
	(3, current_timestamp, current_timestamp + INTERVAL '1 hour', 2, 1),
	(1, current_timestamp, current_timestamp + INTERVAL '1 hour', 2, 2),
	(3, current_timestamp, current_timestamp + INTERVAL '1 hour', 3, 1),
	(2, current_timestamp, current_timestamp + INTERVAL '1 hour', 3, 2),
	(1, current_timestamp, current_timestamp + INTERVAL '1 hour', 4, 1),
	(2, current_timestamp, null, 4, 2)
;