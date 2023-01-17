CREATE TABLE app_user (
	id serial PRIMARY KEY,
	date_created timestamp DEFAULT current_timestamp,
	email_adress varchar NOT NULL,
	first_name varchar,
	last_name varchar
);

CREATE TABLE brain_teaser (
	id serial PRIMARY KEY,
	title varchar NOT NULL,
	description varchar
);

CREATE TABLE bt_anagram_type (
	id serial PRIMARY KEY,
	max_attempts integer,
	time_allowed integer,
	title varchar NOT NULL 
);

CREATE TABLE face_off (
	id serial PRIMARY KEY,
	title varchar,
	date_created timestamp DEFAULT current_timestamp
);

CREATE TABLE bt_anagram (
	id serial PRIMARY KEY,
	anagram_length integer,
	anagram_solution varchar NOT NULL,
	anagram_word varchar NOT NULL,
	date_created timestamp DEFAULT current_timestamp,
	image_location varchar NOT NULL,
	bt_anagram_type_id integer
		REFERENCES bt_anagram_type(id),
	brain_teaser_id integer
		REFERENCES brain_teaser(id)
);

CREATE TABLE app_user_anagram (
	id serial PRIMARY KEY,
	attempts integer,
	date_played timestamp DEFAULT current_timestamp,
	max_attempts integer,
	solved bool,
	time_allowed integer,
	bt_anagram_id integer
		REFERENCES bt_anagram(id),
	app_user_id integer
		REFERENCES app_user(id)
);

CREATE TABLE face_off_user_anagram (
	face_off_id integer
		REFERENCES face_off(id),
	app_user_id integer
		REFERENCES app_user(id),
	bt_anagram_id integer
		REFERENCES bt_anagram(id)
);

--DROP TABLE face_off_user_anagram;
--DROP TABLE app_user_anagram;
--DROP TABLE bt_anagram;
--DROP TABLE bt_anagram_type;
--DROP TABLE face_off ;
--DROP TABLE brain_teaser;
--DROP TABLE app_user;