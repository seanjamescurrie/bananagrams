package coe.bananagrams.dto.game;

import lombok.*;
import java.util.Date;

@Getter
@Setter
@Builder
@NoArgsConstructor
@AllArgsConstructor
public class GameDto {

    private int id;

    private String title;

    private Date dateCreated;

}
