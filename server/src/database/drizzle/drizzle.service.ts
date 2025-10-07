import { Injectable, OnModuleDestroy, OnModuleInit } from '@nestjs/common';
import { drizzle } from 'drizzle-orm/node-postgres';
import * as schema from './schema';
import * as relation from './relation';

import { Pool } from 'pg';

@Injectable()
export class DrizzleService implements OnModuleInit, OnModuleDestroy {
  private pool: Pool;
  public db: ReturnType<typeof drizzle>;

  async onModuleInit() {
    this.pool = new Pool({
      connectionString: process.env.DATABASE_URL,
    });
    this.db = drizzle(this.pool, {
      schema: { ...schema, ...relation },
      logger: true,
      casing: 'snake_case',
    });
    console.log('✅ Drizzle connected');
  }

  async onModuleDestroy() {
    await this.pool.end();
  }
}
