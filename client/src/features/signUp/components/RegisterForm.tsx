import icon from "../../../assets/icons/icon-144x144.png";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { Form, FormControl, FormField, FormItem } from "../../shared/Form";

import { FormInput } from "../../shared/FormInput";
import { PasswordInput } from "../../shared/PasswordInput";
import { registerFormSchema } from "../schema";
import type { RegisterForm } from "../type";
import { GenderType } from "../../shared/types/gender";
import { SelectInput } from "../../shared/SelectInput";

export function RegisterForm() {
  const form = useForm<RegisterForm>({
    resolver: zodResolver(registerFormSchema),
    defaultValues: {
      firstName: "",
      middleName: "",
      lastName: "",
      nickName: "",
      email: "",
      passWord: "",
      confirmPassword: "",
      genderType: GenderType.Male,
      cityId: "",
      dateOfBirth: "",
    },
  });

  const handleSubmit = form.handleSubmit((data) => {
    console.log(data);
  });

  return (
    <>
      <div className="bg-gray-50 dark:bg-gray-800">
        <div className="flex min-h-[80vh] flex-col justify-center py-6 sm:px-6 lg:px-8">
          <div className="flex flex-col items-center text-center sm:mx-auto sm:w-full sm:max-w-md">
            <img src={icon} className="w-16 h-16 my-2" />
            <h1 className="text-3xl my-2 font-extrabold text-gray-900 dark:text-white">
              Register
            </h1>
            <p>Register to have better experience with CSM</p>
          </div>
          <div className="sm:mx-auto sm:w-full sm:max-w-md">
            <div className="bg-white dark:bg-gray-700 pb-4 pt-8 sm:rounded-lg sm:pb-6 sm:shadow">
              <Form {...form}>
                <form onSubmit={handleSubmit} className="space-y-6">
                  {/* FirstName - MiddleName - LastName */}
                  <div className="flex flex-row gap-4">
                    <FormField
                      control={form.control}
                      name="firstName"
                      render={({ field }) => (
                        <FormItem>
                          <FormControl>
                            <FormInput
                              {...field}
                              label="FirstName"
                              requiredMark
                            />
                          </FormControl>
                        </FormItem>
                      )}
                    />

                    <FormField
                      control={form.control}
                      name="middleName"
                      render={({ field }) => (
                        <FormItem>
                          <FormControl>
                            <FormInput
                              {...field}
                              value={field.value ?? undefined}
                              label="MiddleName"
                            />
                          </FormControl>
                        </FormItem>
                      )}
                    />

                    <FormField
                      control={form.control}
                      name="lastName"
                      render={({ field }) => (
                        <FormItem>
                          <FormControl>
                            <FormInput
                              {...field}
                              value={field.value ?? undefined}
                              label="LastName"
                            />
                          </FormControl>
                        </FormItem>
                      )}
                    />
                  </div>

                  {/* Email */}
                  <FormField
                    control={form.control}
                    name="email"
                    render={({ field }) => (
                      <FormItem>
                        <FormControl>
                          <FormInput requiredMark {...field} label="Email" />
                        </FormControl>
                      </FormItem>
                    )}
                  />

                  {/* Password */}
                  <FormField
                    control={form.control}
                    name="passWord"
                    render={({ field }) => (
                      <FormItem>
                        <FormControl>
                          <PasswordInput
                            requiredMark
                            {...field}
                            label="Password"
                          />
                        </FormControl>
                      </FormItem>
                    )}
                  />

                  <FormField
                    control={form.control}
                    name="confirmPassword"
                    render={({ field }) => (
                      <FormItem>
                        <FormControl>
                          <PasswordInput
                            requiredMark
                            {...field}
                            label="Confirm password"
                          />
                        </FormControl>
                      </FormItem>
                    )}
                  />

                  {/* Location */}
                  <div className="flex flex-row gap-2">
                    <div className="flex-1">
                      <FormField
                        control={form.control}
                        name="cityId"
                        render={({ field }) => (
                          <FormItem>
                            <FormControl>
                              <SelectInput
                                options={[]}
                                {...field}
                                label="City"
                                requiredMark
                              />
                            </FormControl>
                          </FormItem>
                        )}
                      />
                    </div>
                  </div>

                  {/* Gender and Date of birth */}
                  <div className="flex flex-row gap-2">
                    <div className="flex-1">
                      <FormField
                        control={form.control}
                        name="genderType"
                        render={({ field }) => (
                          <FormItem>
                            <FormControl>
                              <SelectInput
                                options={[]}
                                {...field}
                                label="Gender"
                                requiredMark
                              />
                            </FormControl>
                          </FormItem>
                        )}
                      />
                    </div>
                  </div>

                  <div className="flex flex-row items-center justify-center">
                    <input
                      id="accept"
                      name="accept"
                      type="checkbox"
                      className="h-4 w-4 rounded border-gray-300 text-indigo-600 focus:ring-indigo-500 dark:text-white dark:border-gray-600 dark:focus:ring-indigo-400 disabled:cursor-wait disabled:opacity-50"
                    />
                    <label
                      htmlFor="accept"
                      className="ml-2 block text-sm text-gray-900 dark:text-white"
                    >
                      I accept with your terms and privacy
                    </label>
                  </div>

                  <div>
                    <button
                      disabled={
                        form.formState.isSubmitting || !form.formState.isValid
                      }
                      type="submit"
                      className="group relative flex w-full justify-center rounded-md border border-transparent bg-blue-500 px-4 py-2 text-sm font-medium text-white hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-indigo-500 focus:ring-offset-2 dark:bg-indigo-700 dark:border-transparent dark:hover:bg-indigo-600 dark:focus:ring-indigo-400 dark:focus:ring-offset-2 disabled:opacity-50"
                    >
                      Sign Up
                    </button>
                  </div>
                </form>
              </Form>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}
