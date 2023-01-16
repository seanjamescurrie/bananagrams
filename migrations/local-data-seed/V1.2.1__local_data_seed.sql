INSERT INTO app_user (date_created, email_adress, first_name, last_name)
VALUES 
	(Current_Timestamp, 'sean.currie@unosquare.com', 'Sean', 'Currie'),
	(Current_Timestamp, 'sean.currie+david@unosquare.com', 'David', 'Currie')
;

INSERT INTO	bt_anagram (anagram_word, date_created, image_location, anagram_length, anagram_solution, bt_anagram_type_id, brain_teaser_id)
VALUES 
	('NANABA', current_timestamp, 'http://tropicalfruitandveg.com/images/bananauk2.jpg', 6, 'BANANA', 1, 1),
	('NSPSAOI RIUTF', current_timestamp, 'http://tropicalfruitandveg.com/images/passionyel.jpg', 12, 'PASSION FRUIT', 2, 1),
	('EHLECY', CURRENT_TIMESTAMP, 'http://tropicalfruitandveg.com/images/lychee2.jpg', 6, 'LYCHEE', 2, 1),
	('PALPPEINE', current_timestamp, 'https://tropicalfruitandveg.com/images/pineapple2.jpg', 9, 'PINEAPPLE', 2, 1)
;

INSERT INTO app_user_anagram (attempts, date_played, solved, time_allowed, max_attempts, bt_anagram_id, app_user_id)
VALUES 
	(2, current_timestamp, TRUE, 0, 5, 1, 1),
	(3, current_timestamp, TRUE, 0, 5, 1, 2),
	(3, current_timestamp, TRUE, 30, 3, 2, 1),
	(1, current_timestamp, TRUE, 30, 3, 2, 2),
	(3, current_timestamp, FALSE, 30, 3, 3, 1),
	(2, current_timestamp, TRUE, 30, 3, 3, 2),
	(1, current_timestamp, TRUE, 30, 3, 4, 1),
	(2, current_timestamp, FALSE, 30, 3, 4, 2)
;

INSERT INTO face_off (title, date_created)
VALUES ('SeanVsDavid', current_timestamp);

INSERT INTO face_off_user_anagram (face_off_id, app_user_id, bt_anagram_id)
VALUES 
	(1, 1, 2),
	(1, 2, 2),
	(1, 1, 3),
	(1, 2, 3),
	(1, 1, 4),
	(1, 2, 4)
;