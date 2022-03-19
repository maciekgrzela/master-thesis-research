BADANIA DO PRACY MAGISTERSKIEJ

Folder zawiera konfiguracje API NodeJS oraz .NET Core dla określonych przypadków testowych:

- konfiguracja 1 (master-thesis-config-1):
badanie porównawcze dotyczące wydajności api napisanych w dwóch technologiach przy wykorzystaniu różnych systemów bazodanowych
	.net core web api with entity framework core and sqlServer/mysql/sqlite
	nodejs api with prisma orm and sqlServer/mysql/sqlite

- konfiguracja 2 (master-thesis-config-2):
badanie porównawcze dotyczące wydajności api napisanych w dwóch technologiach w kontekście obsługi nierelacyjnych baz danych
	.net core web api with mongodb handling
	nodejs api with mongoose and mongodb

- konfiguracja 3 (master-thesis-config-3):
badanie porównawcze wydajności pomiędzy modelem relacyjnym a nierelacyjnym (implementacja niepotrzebna - na podstawie danych z dwóch poprzednich scenariuszy badawczych

- konfiguracja 4 (master-thesis-config-4):
badanie porównawcze wydajności dla realizacji złożonych obliczeń (uruchomienie algorytmu metaheurystycznego wykonywanego w stałym czasie) w celu określenia możliwości badanych technologii do obsługi poleceń współbieżnych oraz dokonywania wewnętrznych optymalizacji
	.net core web api with tsp from mf
	nodejs api with tsp written based on mf
	
- konfiguracja 5 (master-thesis-config-5):
badanie porównawcze dotyczące charakterystyki działania mechanizmów pamięci podręcznej implementowanych domyślnie w ramach badanych api. Wygenerowanie serii 10/20 lub 30 żądań o takiej samej treści od jednego klienta i obserwowanie zmienności czasów odpowiedzi
	.net core web api z konfiguracji 1
	nodejs api z konfiguracji 1
	
- konfiguracja 6 (master-thesis-config-6):
badanie porównawcze wydajności api dla analizowanych technologii po wdrożeniu ich na wirtualny serwer prywatny, a także na dedykowane środowiska (tj. azure cloud / heroku) - sprawdzenie na ile zmienia się wydajność działania jeśli api postawione jest w standardowym i dedykowanym środowisku
	.net core web api z danymi przechowywanymi inMemory i mechanizmem mierzącym czas przetwarzania (czas responsu - zwrot do klienta)
	.nodejs api z danymi przechowywanymi inMemory i mechanizmem mierzącym czas przetwarzania (czas responsu - zwrot do klienta)

POMYSŁY NA DODATKOWE BADANIA:
- porównanie wydajności z wykorzystaniem klasycznej architektury 3-warstwowej a także zastosowaniem wzorca projektowego CQRS
- zdefiniowanie własnego systemu pamięci podręcznej wewnątrz api i sprawdzenie zmiany wydajności po jego wprowadzeniu
- 