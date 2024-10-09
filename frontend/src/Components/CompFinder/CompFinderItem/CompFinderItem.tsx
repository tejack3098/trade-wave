import React from 'react'
import { Link } from 'react-router-dom';

type Props = {
    ticker: string;
}

const CompFinderItem = ({ticker}: Props) => {
  return <Link 
    reloadDocument
    to={`/company/${ticker}/company-profile`}  
    type="button"
    className="flex items-center p-3 text-base font-normal text-gray-900 rounded-lg hover:bg-gray-100 hover:text-blue-700" >   
    {ticker}
  </Link>
}

export default CompFinderItem