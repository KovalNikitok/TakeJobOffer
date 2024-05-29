interface Props{
    name: string;
    description: string;
}

export const CardTitle =({name, description}: Props) =>{
    return (
        <div className="card__style">
            <p className="card__name">{name}</p>
            <p className="card__description">{description}</p>
        </div>
    )
};