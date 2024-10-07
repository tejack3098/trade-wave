import React, { ChangeEvent, SyntheticEvent, useState } from 'react';
import logo from './logo.svg';
import './App.css';
import Card from './Components/Card/Card';
import CardList from './Components/CardList/CardList';
import Search from './Components/Search/Search';
import { CompanySearch } from './company';
import { searchCompanies } from './api';
import { on } from 'events';
import ListPortfolio from './Components/Portfolio/ListPortfolio/ListPortfolio';
import Navbar from './Components/Navbar/Navbar';
import Hero from './Components/Hero/Hero';

function App() {
  const [search, setSearch] = useState<string>("");
  const [portfolioValues, setPortfolioValues] = useState<string[]>([]);
  const [searchResult, setSearchResult] = useState<CompanySearch[]>([]);
  const [serverError, setServerError] = useState<string | null>(null);
    
  const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
      setSearch(e.target.value);
  }

  const onClick = async (e: SyntheticEvent) => {
    const result = await searchCompanies(search);
    if(typeof result === "string"){
      setServerError(result);
    }
    else if (Array.isArray(result.data)){
      setSearchResult(result.data);
    }

    console.log(searchResult);
    
  };

  const onPortfolioCreate = (e: any) => {
    e.preventDefault();
    const exits = portfolioValues.find((value) => value === e.target[0].value);
    if(exits)return;
    const updatedPortfolio = [...portfolioValues, e.target[0].value];
    setPortfolioValues(updatedPortfolio);
  }

  const onPortfolioDelete = (e: any) => {
    e.preventDefault();
    const updatedPortfolio = portfolioValues.filter((value) => value !== e.target[0].value);
    setPortfolioValues(updatedPortfolio);
  }

  return (
    <div className="App">
      <Navbar />
      {/* <Hero />  */}
      <Search onClick={onClick} search={search} handleChange={handleChange}/>
      {serverError && <h1>{serverError}</h1>}
      <ListPortfolio portfolioValues={portfolioValues} onPortfolioDelete={onPortfolioDelete}/>
      <CardList searchResults={searchResult}  onPortfolioCreate={onPortfolioCreate}/>
    </div>
  );
}

export default App;