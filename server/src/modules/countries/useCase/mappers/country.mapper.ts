import { Mapper } from 'src/libs/ddd/mapper.interface';
import { CountryEntity } from '../../core/country.entity';
import {
  CountryModel,
  countrySchema,
} from '../../infrastructure/country.types';
import { Injectable } from '@nestjs/common';
import { CountryResponseDto } from '../dtos/country.response.dto';

@Injectable()
export class CountryMapper
  implements Mapper<CountryEntity, CountryModel, CountryResponseDto>
{
  toPersistence(entity: CountryEntity): CountryModel {
    const id = entity.getId();
    const props = entity.getProps();
    const record: CountryModel = {
      id: +id,
      name: props.name,
    };

    return countrySchema.parse(record);
  }
  toDomain(record: CountryModel): CountryEntity {
    const entity = new CountryEntity(record.id.toString(), {
      name: record.name,
    });

    return entity;
  }

  toResponse(entity: CountryEntity): CountryResponseDto {
    return {
      id: +entity.getId(),
      name: entity.getProps().name,
    };
  }
}
