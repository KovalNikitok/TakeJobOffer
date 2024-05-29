import Card from "antd/es/card/Card"
import Button from "antd/es/button/button"

import { CardTitle } from "../../shared/ui/Cardtitle"

interface Props {
    professions: ProfessionWithSlug[];
    handleOpen: (profession: ProfessionWithSlug) => void;
    handleDelete: (id: string) => void;
}

export const Professions = ({professions, handleOpen, handleDelete}: Props) => {
    return (
        <div className="cards">
            {professions.map((profession: ProfessionWithSlug) => (
                <Card key={profession.id} title={
                        <CardTitle name={profession.name} description={profession?.slug}/>
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
};