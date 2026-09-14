import { APITester } from "./APITester";
import "./index.css";

import logo from "./logo.svg";
import reactLogo from "./react.svg";
import {useEffect, useState} from "react";
import {Api, type Book} from "@/api/Api.ts";

export const MyBackendAlwaysUseThisOneVeryImportant = new Api();

export function App() {

    const [books, setBooks] = useState<Book[]>([])

    useEffect(() => {
        MyBackendAlwaysUseThisOneVeryImportant.getBooks.libraryGetBooks().then(r => {
            setBooks(r)
        })
    }, []);

  return (
    <div className="app">

        {
            books.map(b => {
                return <div>Book title: {b.bookTitle}</div>
            })
        }

    </div>
  );
}

export default App;
