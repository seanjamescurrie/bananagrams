package coe.bananagrams.entity;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.Id;
import jakarta.persistence.Table;
import lombok.*;

@Getter
@Setter
@Entity
@Builder
@NoArgsConstructor
@AllArgsConstructor
@Table(name = "game_anagram_types")
public class GameAnagramType {
    @Id
    @Column(name = "id", updatable = false, nullable = false)
    private int id;
    @Column(name ="max_attempts", nullable = false)
    private int maxAttempts;
    @Column(name = "title", nullable = false)
    private String title;
    @Column(name = "time_allowed", nullable = false)
    private int timeAllowed;
}
