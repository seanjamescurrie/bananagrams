package coe.bananagrams.entity;

import jakarta.persistence.*;
import lombok.*;

import java.util.Date;

@Getter
@Setter
@Entity
@Builder
@NoArgsConstructor
@AllArgsConstructor
@Table(name = "game_user_game_anagrams")
public class GameUserGameAnagram {
    @Id
    @Column(name = "id", updatable = false, nullable = false)
    private int id;
    @Column(name = "date_played", nullable = true)
    private Date datePlayed;
    @Column(name = "date_solved", nullable = true)
    private Date dateSolved;
    @Column(name = "game_user_id", insertable = false, updatable = false, nullable = false)
    private int gameUserId;
    @Column(name = "game_anagram_id", insertable = false, updatable = false, nullable = false)
    private int gameAnagramId;

    @ManyToOne()
    private GameUser gameUser;
    @ManyToOne()
    private GameAnagram gameAnagram;
}
