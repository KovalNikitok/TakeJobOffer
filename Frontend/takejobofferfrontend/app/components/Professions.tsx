import Card from "antd/es/card/Card"
import { CardTitle } from "./Cardtitle"
import Button from "antd/es/button/button"

interface Props {
    professions: Profession[];
    handleOpen: (profession: Profession) => void;
    handleDelete: (id: string) => void;
}

export const Professions = ({professions, handleOpen, handleDelete}: Props) => {
    return (
        <div className="cards">
            {professions.map((profession: Profession) => (
                <Card key={profession.id} title={
                        <CardTitle name={profession.name} description={profession.description}/>
                    }
                    bordered={false}
                >
                    <p>{profession.description}</p>
                    <div className="card__buttons">
                        <Button 
                            onClick={() => handleOpen(profession)}
                            style={{flex: 1}}
                        >
                            Edit
                        </Button>
                        <Button
                            danger
                            onClick={() => handleDelete(profession.id)}
                            style={{flex: 1}}
                        >
                            Delete
                        </Button>
                    </div>
                </Card>
            ))}
        </div>
    )
}