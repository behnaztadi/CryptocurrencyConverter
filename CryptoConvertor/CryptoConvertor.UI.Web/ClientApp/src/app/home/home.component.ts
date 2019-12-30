import { Component, OnInit } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { QuoteHttpService } from './quote.httpservice';
import { quoteResultDto } from './model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {

  public cryptoCurrencies: string[];
  public selectedCryptoCurrency: string = "";
  public quotes;

  constructor(private quoteHttpService: QuoteHttpService) {
    // TODO: Load the list from API
    this.cryptoCurrencies = ["BTC", "ETH", "USDT", "ZEC", "PPC"];
  }

  onShow(): void {
    this.quoteHttpService.getQuote(this.selectedCryptoCurrency);
  } 

  ngOnInit(): void {

    // TODO : Move to a seprate service   
    const connection = new signalR.HubConnectionBuilder()
      .configureLogging(signalR.LogLevel.Information)
        .withUrl("http://localhost:5010/notify")
      .build();

    connection.start().then(function () {
      console.log('Connected!');
    }).catch(function (err) {
      return console.error(err.toString());
    });

    connection.on("BroadcastMessage", (type: string, payload: string) => {
      this.quotes = JSON.parse(payload);
    });
  }
}
