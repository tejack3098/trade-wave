import React, { ChangeEvent, SyntheticEvent, useState } from 'react';
import logo from './logo.svg';
import './App.css';
import Card from './Components/Card/Card';
import CardList from './Components/CardList/CardList';
import Search from './Components/Search/Search';
import { CompanySearch } from './company';

function App() {
  const [search, setSearch] = useState<string>("");
  const [searchResult, setSearchResult] = useState<CompanySearch[]>([]);
  const [serverError, setServerError] = useState<string>("");
    
  const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
      setSearch(e.target.value);
      console.log(e.target.value);
  }

  const onClick = async (e: SyntheticEvent) => {
    const result = await searchCompanies(search);
    if(typeof result === "string"){
      setServerError(result);
    }
    else if (typeof result === "object")
    setSearchResult(result.data);
  };


  return (
    <div className="App">
      <Search onClick={onClick} search={search} handleChange={handleChange}/>
      <CardList />
    </div>
  );
}

export default App;
function searchCompanies(search: string) {
  throw new Error('Function not implemented.');
}

