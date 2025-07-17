import { z } from "zod";
import { registerFormSchema } from "./schema";

export type RegisterForm = z.infer<typeof registerFormSchema>;
