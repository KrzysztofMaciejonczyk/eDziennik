export interface DataOpi {
  data_opis_id: number;
  data: string;
  opis: string;
}

export interface Klasa {
  nauczyciel_id: number;
  klasa_id: number;
  nazwa: string;
}

export interface Nauczyciel {
  osoba_id: number;
  nauczyciel_id: number;
}

export interface Obecnosc {
  czy_byl: boolean;
  uczen_id: number;
  data_opis_id: number;
  ocena_id: number;
}

export interface Ocena {
  ocena_n: number;
  nauczyciel_id: number;
  uczen_id: number;
  data_opis_id: number;
  ocena_id: number;
}

export interface Osoba {
  osoba_id: number;
  imie: string;
  email: string;
  nazwisko: string;
  telefon: string;
  haslo: string;
}

export interface Przedmiot {
  nauczyciel_id: number;
  klasa_id: number;
  przedmiot_id: number;
  nazwa: string;
}

export interface Rodzic {
  osoba_id: number;
  rodzic_id: number;
}

export interface Uczen {
  data_urodzenia: string;
  osoba_id: number;
  rodzic_id: number;
  klasa_id: number;
  uczen_id: number;
  adres: string;
}

export interface Uwaga {
  nauczyciel_id: number;
  uczen_id: number;
  data_opis_id: number;
  uwaga_id: number;
}
