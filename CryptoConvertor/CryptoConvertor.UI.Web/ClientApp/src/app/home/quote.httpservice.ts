import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map, catchError, tap } from 'rxjs/operators';
import { quoteResultDto } from './model';



@Injectable()
export class QuoteHttpService {

    constructor(private http: HttpClient) {
    }

    getQuote(cryptoCurrency: string): void {
        this.http.get<quoteResultDto[]>('http://localhost:5010/api/Quotes?cryptoCurrency=' + cryptoCurrency).subscribe(result => {    

        }, error => console.error(error));
    }
}
