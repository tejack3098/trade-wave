import React from 'react';
import './Card.css';

interface Props  {
  companyName: string;
  ticker: string; 
  price: number;
}

const Card: React.FC<Props> = ({companyName, ticker, price}: Props): JSX.Element => {
  return (
    <div className="card">
        <img width={200} height={200} src="https://images.unsplash.com/photo-1531554694128-c4c6665f59c2?q=80&w=987&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D" alt="Image" />  
        <div className="details">
            <h2>{companyName} ({ticker})</h2> 
            <p>${price}</p>   
        </div>  
        <p className="info">
            Apple is an American multinational technology company headquartered 
            in Cupertino, California. Apple is the world's largest personal computer brand.
        </p>
    </div>
  )
}

export default Card;