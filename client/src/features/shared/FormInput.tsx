import { useFormField } from "./Form";
import { cn } from "../../lib/cn";
import { forwardRef, type InputHTMLAttributes } from "react";

interface FormInputProps extends InputHTMLAttributes<HTMLInputElement> {
  label: string;
  requiredMark: boolean;
}

export const FormInput = forwardRef<HTMLInputElement, FormInputProps>(
  ({ label, requiredMark, className, ...props }, ref) => {
    const { formItemId, formDescriptionId, formMessageId, error } =
      useFormField();

    return (
      <div className="relative w-full">
        {label && (
          <label
            htmlFor={formItemId}
            className="absolute -top-2 left-3 bg-white px-1 text-xs text-gray-500 dark:bg-gray-800 dark:text-gray-400"
          >
            {label}
            {requiredMark && <span className="text-red-500">*</span>}
          </label>
        )}
        <input
          ref={ref}
          id={formItemId}
          aria-describedby={
            !error ? formDescriptionId : `${formDescriptionId} ${formMessageId}`
          }
          aria-invalid={!!error}
          className={cn(
            "w-full rounded border border-gray-300 px-3 pt-4 pb-2 text-sm text-gray-900 focus:border-blue-500 focus:outline-none focus:ring-1 focus:ring-blue-500 dark:bg-gray-800 dark:text-white",
            className
          )}
          {...props}
        />
      </div>
    );
  }
);
