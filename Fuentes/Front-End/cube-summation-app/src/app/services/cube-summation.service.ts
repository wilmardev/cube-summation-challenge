import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Util } from '../util/util';
import { Observable, throwError } from 'rxjs';
import { RespuestaGeneral } from '../models/response-model';
import { catchError } from 'rxjs/operators';

@Injectable()
export class CubeSummationService {

  util: Util = new Util();

  constructor(private httpClient: HttpClient) { }

  sendDataCube(modelo: any): Observable<RespuestaGeneral> {
    const httParams = new HttpParams()
      .append('Accept', 'text/plain');

    return this.httpClient
      .post<RespuestaGeneral>(this.util.baseUrl + 'CubeSummation/ProcesarInformacion', modelo.dataInput)
      .pipe(catchError(error => {
        return throwError(error);
      }));
  }
}
