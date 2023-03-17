INSERT INTO users (date_created, email_address, first_name, last_name, password, username)
VALUES 
	(Current_Timestamp - INTERVAL '1 DAY', 'sean.currie@unosquare.com', 'Sean', 'Currie', '$2a$11$W.4VI.33LKr9Z2uA6kh/E.iqHHs.ue28aIrzHVZtqYmlRpGIdFc2y', 'seancurrie'),
	(Current_Timestamp - INTERVAL '1 DAY', 'sean.currie+david@unosquare.com', 'David', 'Currie', '$2a$11$W.4VI.33LKr9Z2uA6kh/E.iqHHs.ue28aIrzHVZtqYmlRpGIdFc2y', 'davidcurrie')
;

INSERT INTO games (title, date_created)
VALUES
	('Sean Vs David', current_timestamp - INTERVAL '1 DAY'),
	('Daily 12/12/2023', current_timestamp - INTERVAL '1 DAY')
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
	('NANABA', current_timestamp - INTERVAL '1 DAY', 1, 1, 1, 1),
	('NSPSAOI RIUTF', current_timestamp - INTERVAL '1 DAY', 1, 2, 2, 2),
	('EHLECY', CURRENT_TIMESTAMP - INTERVAL '1 DAY', 2, 2, 3, 2),
	('PALPPEINE', current_timestamp - INTERVAL '1 DAY', 3, 2, 4, 2)
;

INSERT INTO game_user_game_anagrams (attempts, date_played, date_solved, game_anagram_id, game_user_id)
VALUES 
	(2, current_timestamp - INTERVAL '1 DAY', current_timestamp - INTERVAL '1 DAY', 1, 1),
	(3, current_timestamp - INTERVAL '1 DAY', current_timestamp - INTERVAL '1 DAY', 1, 2),
	(3, current_timestamp - INTERVAL '1 DAY', current_timestamp - INTERVAL '1 DAY', 2, 1),
	(1, current_timestamp - INTERVAL '1 DAY', current_timestamp - INTERVAL '1 DAY', 2, 2),
	(3, current_timestamp - INTERVAL '1 DAY', current_timestamp - INTERVAL '1 DAY', 3, 1),
	(2, current_timestamp - INTERVAL '1 DAY', current_timestamp - INTERVAL '1 DAY', 3, 2),
	(1, current_timestamp - INTERVAL '1 DAY', current_timestamp - INTERVAL '1 DAY', 4, 1),
	(2, current_timestamp - INTERVAL '1 DAY', NULL, 4, 2)
;