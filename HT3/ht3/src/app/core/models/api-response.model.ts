import { ApiErrorTypeEnum } from "../enums/api-error-type.enum";

export interface ApiResponse<T = any> {
    isSuccess: boolean;
    error?: ApiErrorResponse;
    data?: T;
}

export interface ApiErrorResponse<TError = any> {
    message: string;
    error: TError;
    errorType: ApiErrorTypeEnum;
}
