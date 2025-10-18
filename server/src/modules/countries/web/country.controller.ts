import {
  Body,
  Controller,
  Get,
  Param,
  ParseIntPipe,
  Post,
} from '@nestjs/common';
import { CreateCountryUseCase } from '../useCase/features/createCountry/create-country.usecase';
import { CreateCountryRequest } from '../useCase/features/createCountry/create-country.dto';
import { GetCountriesUseCase } from '../useCase/features/getCountries/get-countries.usecase';
import { GetCountryByIdUsecase } from '../useCase/features/getCountryById/get-country-by-id.usecase';

@Controller('countries')
export class CountryController {
  constructor(
    private readonly createCountryUsecase: CreateCountryUseCase,
    private readonly getCountriesUsecase: GetCountriesUseCase,
    private readonly getCountryByIdUsecase: GetCountryByIdUsecase,
  ) {}

  @Post()
  async createCountry(@Body() dto: CreateCountryRequest) {
    return this.createCountryUsecase.execute(dto);
  }

  @Get()
  async getCountries() {
    return this.getCountriesUsecase.execute();
  }

  @Get(':id')
  async getCountryById(@Param('id', ParseIntPipe) id: number) {
    return this.getCountryByIdUsecase.execute(id);
  }
}
