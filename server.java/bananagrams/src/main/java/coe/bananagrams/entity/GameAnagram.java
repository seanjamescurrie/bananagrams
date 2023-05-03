package coe.bananagrams.entity;

import jakarta.persistence.*;
import lombok.*;

import java.util.Date;
import java.util.List;

@Getter
@Setter
@Entity
@Builder
@NoArgsConstructor
@AllArgsConstructor
@Table(name = "game_anagrams")
public class GameAnagram {
    @Id
    @Column(name = "id", updatable = false, nullable = false)
    private int id;
    @Column(name = "anagram_word", nullable = false)
    private String anagramWord;
    @Column(name = "date_created", nullable = false)
    private Date dateCreated;
    @Column(name = "order_sequence", nullable = false)
    private int order;
    @Column(name = "game_id", insertable = false, updatable = false, nullable = false)
    private int gameId;
    @Column(name = "game_anagram_type_id", insertable = false, updatable = false, nullable = false)
    private int gameAnagramTypeId;
    @Column(name = "word_id", insertable = false, updatable = false, nullable = false)
    private int wordId;

    @ManyToOne()
    private Game game;
    @ManyToOne()
    private GameAnagramType gameAnagramType;
    @ManyToOne()
    private Word word;
    @OneToMany(mappedBy = "gameAnagram")
    private List<GameUserGameAnagram> gameUserGameAnagrams;
}
