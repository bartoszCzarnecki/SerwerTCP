# Serwer Rejesteracji Użytkowników

## Wymagania Fukcjonalne

1. Administrator uruchamia aplikację serwera.
2. Serwer rozpoczyna działanie od razu po uruchomieniu aplikacji.
3. Administrator kończy działanie aplikacji serwera.
4. Klient łączy się z aplikacją serwera.
5. Klient loguje się za pomocą loginu i hasła.
6. Klient kończy połączenie z aplikacją serwera.
7. Możliwość rejestracji nowych użytkowników - klient podaje login i hasło.
8. Możliwość sprawdzenia danych logowania - klient podaje login i hasło.
9. Czytanie użytkowników z pliku tekstowego.
10. Zapisywanie użytkowników do pliku tekstowego.
11. Możliwość zmiany hasła przez klienta.
12. Sprawdzenie rodzaju trójkąta o podanych długościach boków.
13. Usuwanie konta.

## Wymagania Pozafunkcjonalne

1. Aplikacja serwera jest dostarczona w postaci aplikacji konsolowej przeznaczonej na system Windows.(GUI –sposób dostarczenia funkcjonalności).
2. W komunikacjiklient-serwerwykorzystywany jest protokół komunikacyjny Raw –wiadomości przesyłane są bezpośrednio (bezpieczeństwo).
3. W ramach serwera nie jest implementowana obsługa rozłączającego się klienta(niezawodność).
4. W ramach serwera nie jest implementowana informacja o wyłączeniu serwera przysłana do klienta (niezawodność).
5. Serwer obsługuje wielu klientów na raz.
6. Rozłączenie się z serwerem przez jednego klienta nie wpływa na połączenie z innymi klientami.
