package coe.bananagrams.mapper;

import java.util.List;
import java.util.Objects;
import java.util.Set;
import java.util.stream.Collectors;
import java.util.stream.StreamSupport;
import org.modelmapper.ModelMapper;

public class Mapper extends ModelMapper {

    public <T, U> void addSourceToTargetMapping(final SourceToTargetMapping<T, U> mapping) {

        Objects.requireNonNull(mapping);
        this.addMappings(mapping.mapFromSourceToTarget());
    }

    public <T, U> void addTargetToSourceMapping(final TargetToSourceMapping<T, U> mapping) {

        Objects.requireNonNull(mapping);
        this.addMappings(mapping.mapFromTargetToSource());
    }

    public <D, T> List<D> map(final Iterable<T> entityList, Class<D> outCLass) {

        Objects.requireNonNull(entityList);
        Objects.requireNonNull(outCLass);

        return StreamSupport.stream(entityList.spliterator(), true)
                .map(entity -> map(entity, outCLass))
                .collect(Collectors.toList());
    }

    public <D, T> Set<D> map(final Set<T> entityList, Class<D> outCLass) {

        Objects.requireNonNull(entityList);
        Objects.requireNonNull(outCLass);

        return entityList.stream()
                .map(entity -> map(entity, outCLass))
                .collect(Collectors.toSet());
    }
}