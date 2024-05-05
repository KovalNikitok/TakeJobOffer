interface Props{
    name: string;
    description: string;
}

export const CardTitle =({name, description}: Props) =>{
    return (
        <div style={{
            display: "flex",
            flexDirection: "row",
            alignItems: "center",
            justifyContent: "space-between",
        }}>
            <p className="card__name">{name}</p>
            <p className="card__description">{description}</p>
        </div>
    )
};