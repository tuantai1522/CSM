import { useFormField } from "./Form";
import { cn } from "../../lib/cn";
import { forwardRef, useState, type InputHTMLAttributes } from "react";
import { Eye, EyeOff } from "lucide-react";

interface PasswordInputProps extends InputHTMLAttributes<HTMLInputElement> {
  label: string;
  requiredMark?: boolean;
}

export const PasswordInput = forwardRef<HTMLInputElement, PasswordInputProps>(
  ({ label, requiredMark = false, className, ...props }, ref) => {
    const { formItemId, formDescriptionId, formMessageId, error } =
      useFormField();

    const [showPassword, setShowPassword] = useState(false);

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
          type={showPassword ? "text" : "password"}
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

        <button
          type="button"
          className="absolute right-2 top-1/2 -translate-y-1/2 text-gray-500 hover:text-gray-700 dark:text-gray-400 dark:hover:text-gray-200"
          onClick={() => setShowPassword((prev) => !prev)}
          tabIndex={-1}
        >
          {showPassword ? <EyeOff size={18} /> : <Eye size={18} />}
        </button>

        {error && (
          <p id={formMessageId} className="mt-1 text-sm text-red-500">
            {error.message}
          </p>
        )}
      </div>
    );
  }
);
