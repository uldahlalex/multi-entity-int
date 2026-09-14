import { APITester } from "./APITester";
import "./index.css";

import logo from "./logo.svg";
import reactLogo from "./react.svg";
import {useEffect, useState} from "react";
import {Api, type Book} from "@/api/Api.ts";

export const MyBackendAlwaysUseThisOneVeryImportant = new Api();

export function App() {

    const [books, setBooks] = useState<Book[]>([])
    const [newTitle, setNewTitle] = useState("")

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
        <input placeholder={"make title new a new book"} onChange={e => setNewTitle(e.target.value)} value={newTitle}  />
        <button onClick={() => {
            MyBackendAlwaysUseThisOneVeryImportant.createBook.libraryCreateBook({title: newTitle}).then(r => {
                MyBackendAlwaysUseThisOneVeryImportant.getBooks.libraryGetBooks().then(r => {
                    setBooks(r)
                })
                //if success (meaning if 200-something status code response from the backend)
            }).catch(e => {
                //if failure (meaning if 400 or 500-something status codes get you into this block
            })
        }}>Click to create new book</button>

    </div>
  );
}

export default App;
