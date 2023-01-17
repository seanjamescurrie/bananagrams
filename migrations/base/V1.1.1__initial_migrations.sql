CREATE TABLE users (
	id serial PRIMARY KEY,
	date_created timestamp DEFAULT current_timestamp,
	email_adress varchar NOT NULL,
	first_name varchar,
	last_name varchar,
	user_name varchar NOT NULL
);

CREATE TABLE games (
	id serial PRIMARY KEY,
	title varchar,
	date_created timestamp DEFAULT current_timestamp
);

CREATE TABLE game_users (
	id serial PRIMARY KEY,
	game_id integer
		REFERENCES games(id),
	user_id integer
		REFERENCES users(id)
);

CREATE TABLE words (
	id serial PRIMARY KEY,
	title varchar NOT NULL,
	description varchar NOT NULL,
	image_location varchar NOT NULL
);

CREATE TABLE daily_words (
	id serial PRIMARY KEY,
	anagram varchar NOT NULL,
	date_created timestamp DEFAULT current_timestamp,
	word_id integer
		REFERENCES words(id)
);

CREATE TABLE game_anagram_types (
	id serial PRIMARY KEY,
	max_attempts integer,
	time_allowed integer,
	title varchar NOT NULL 
);

CREATE TABLE game_anagrams (
	id serial PRIMARY KEY,
	anagram_word varchar NOT NULL,
	date_created timestamp DEFAULT current_timestamp,
	order_sequence integer,
	game_id integer
		REFERENCES games(id),
	word_id integer
		REFERENCES words(id),
	game_anagram_type_id integer
		REFERENCES game_anagram_types(id)
);

CREATE TABLE game_user_game_anagrams (
	id serial PRIMARY KEY,
	attempts integer,
	date_played timestamp DEFAULT current_timestamp,
	date_solved timestamp,
	time_allowed integer,
	game_user_id integer
		REFERENCES game_users(id),
	game_anagram_id integer
		REFERENCES game_anagrams(id)
);


--DROP TABLE game_user_game_anagrams;
--DROP TABLE game_users;
--DROP TABLE game_anagrams;
--DROP TABLE game_anagram_types;
--DROP TABLE daily_words;
--DROP TABLE users;
--DROP TABLE games;
--DROP TABLE words;