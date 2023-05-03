package coe.bananagrams.entity;

import jakarta.persistence.*;
import lombok.*;

import java.util.Date;
import java.util.Set;

@Getter
@Setter
@Entity
@Builder
@NoArgsConstructor
@AllArgsConstructor
@Table(name = "games")
public class Game {

    @Id
    @Column(name = "id", updatable = false, nullable = false)
    private int id;

    @Column(name = "title", nullable = false)
    private String title;

    @Column(name = "date_created", nullable = false)
    private Date dateCreated;

    @OneToMany(mappedBy = "game")
    private Set<GameAnagram> gameAnagrams;
    @OneToMany(mappedBy = "game")
    private Set<GameUser> gameUsers;

}
