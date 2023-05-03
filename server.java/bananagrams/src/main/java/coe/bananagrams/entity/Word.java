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
@Table(name = "words")
public class Word {
    @Id
    @Column(name = "id", updatable = false, nullable = false)
    private int id;
    @Column(name = "description", nullable = false)
    private String description;
    @Column(name = "image_location", nullable = false)
    private String imageLocation;
    @Column(name = "title", nullable = false)
    private String title;
}
