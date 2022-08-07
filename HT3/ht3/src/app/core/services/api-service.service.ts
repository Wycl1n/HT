import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import * as moment from "moment";
import { of, throwError, iif, Observable, OperatorFunction, switchMap, tap } from "rxjs";

import { environment } from "../../../environments/environment";
import { ApiResponse } from "../models/api-response.model";

@Injectable({
    providedIn: 'root'
  })
export class ApiService {
    private baseUrl = environment.webApi;

    public constructor(
        public http: HttpClient,
        private router: Router
    ) {}

    public get<T>(
        url: string,
        options?: {
            headers?: HttpHeaders;
            params?: HttpParams;
        }
    ): Observable<ApiResponse<T>> {
        options ??= {};
        
        return this.http.get<ApiResponse<T>>(this.baseUrl + url, options);
    }

    public post<T>(
        url: string,
        body?: any,
        options?: {
            headers?: HttpHeaders;
            params?: HttpParams;
        }
    ): Observable<ApiResponse<T>> {
        options ??= {};

        return this.http.post<ApiResponse<T>>(this.baseUrl + url, body, options);
    }

    public put<T>(
        url: string,
        body: any,
        options?: {
            headers?: HttpHeaders
        }
    ): Observable<ApiResponse<T>> {
        options ??= {};

        return this.http.put<ApiResponse<T>>(this.baseUrl + url, body, options);
    }

    public delete<T>(
        url: string,
        options?: {
            headers?: HttpHeaders;
            params?: HttpParams;
            body?: any;
        }
    ): Observable<ApiResponse<T>> {
        options ??= {};

        return this.http.delete<ApiResponse<T>>(this.baseUrl + url, options);
    }
}

export function extractData<T>(parseDate: boolean = false): OperatorFunction<any, any> {
    return (source: Observable<ApiResponse<T>>) =>
        source.pipe(
            switchMap(x => iif(() => x.isSuccess, of(x.data), throwError(x.error))),
            tap(data => (parseDate || !environment.production) && processResponse(data, parseDate))
        );
}

function processResponse(data: any, parseDate: boolean) {
    if (!data) {
        return;
    }

    for (const key of Object.keys(data)) {
        const value = data[key];
        const momentDate = typeof value === typeof '' ? moment.utc(value, moment.defaultFormatUtc, true) : null;
        if (momentDate?.isValid()) {
            data[key] = momentDate.toDate();
        } else if (Array.isArray(value)) {
            value.forEach(x => processResponse(x, parseDate));
        } else if (typeof value === typeof {}) {
            processResponse(value, parseDate);
        }
    }
}

export function formatString(source: string, args: { [key: string]: string | number | boolean } | Array<string | number>): string {
	if (!source || source === '')
		return source;

	return Array.isArray(args)
		? args.reduce((accumulator: string, value, index) => accumulator.replace(`{${index}}`, value?.toString()), source) as string
		: Object.entries(args).reduce((accumulator, [key, value]) => accumulator.replace(`{${key}}`, value?.toString()), source);
}


const booksArea = 'books'
const recommended = 'recommended'
const rate = 'rate'
const review = 'review'

const saveActions = 'save'

export const api = {
    books: {
        getAll: `${booksArea}`,
        getModelById: `${booksArea}/{id}`,
        createOtUpdate: `${booksArea}/${saveActions}`
    },
    recommended: {
        getTopTen: `${recommended}`
    }
}