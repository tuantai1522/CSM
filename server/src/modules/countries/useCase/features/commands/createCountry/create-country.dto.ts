import { IsNotEmpty, IsString } from 'class-validator';

export class CreateCountryRequest {
  @IsNotEmpty()
  @IsString()
  name!: string;
}
