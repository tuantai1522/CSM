import { IsInt, Min } from 'class-validator';

export class GetCountryByIdRequest {
  @IsInt()
  @Min(1)
  id: number;
}
