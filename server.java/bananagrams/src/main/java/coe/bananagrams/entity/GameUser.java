package coe.bananagrams.entity;

import jakarta.annotation.Nullable;
import jakarta.persistence.*;
import lombok.*;

import java.util.List;
import java.util.Set;

@Getter
@Setter
@Entity
@Builder
@NoArgsConstructor
@AllArgsConstructor
@Table(name = "game_users")
public class GameUser {
    @Id
    @Column(name = "id", updatable = false, nullable = false)
    private int id;
    @Column(name = "game_id", insertable = false, updatable = false, nullable = false)
    private int gameId;
    @Column(name = "user_id", insertable = false, updatable = false, nullable = false)
    private int userId;

    @ManyToOne
    private Game game;
    @ManyToOne
    private User user;
    @OneToMany(mappedBy = "gameUser")
    private Set<GameUserGameAnagram> gameUserGameAnagrams;
}
