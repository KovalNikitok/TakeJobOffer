import Card from "antd/es/card/Card"
import { CardTitle } from "./Cardtitle"
import Button from "antd/es/button/button"

interface Props {
    professions: Profession[];
    handleDetailed: (profession: Profession) => void;
}

export const Professions = ({professions, handleDetailed}: Props) => {
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
                            onClick={() => handleDetailed(profession)}
                            style={{flex: 1}}
                        >
                            Detailed
                        </Button>
                    </div>
                </Card>
            ))}
        </div>
    )
};