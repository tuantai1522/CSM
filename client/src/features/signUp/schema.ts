import { z } from "zod";
import { GenderType } from "../shared/types/gender";

export const registerFormSchema = z
  .object({
    email: z.email(),
    nickName: z.string().nonempty(),
    firstName: z.string().nonempty(),
    middleName: z.string().nullable(),
    lastName: z.string().nullable(),
    passWord: z.string().min(8),
    confirmPassword: z.string().min(8),
    dateOfBirth: z.iso.date(),
    cityId: z.ulid().nonempty(),
    genderType: z.enum(GenderType),
  })

  // To compare password and confirmPassword
  .refine((data) => data.passWord === data.confirmPassword, {
    path: ["confirmPassword"],
    message: "Mật khẩu và xác nhận không khớp",
  });
