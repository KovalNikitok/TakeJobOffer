import Card from "antd/es/card/Card"
import { CardTitle } from "./Cardtitle"
import Button from "antd/es/button/button"
import Link from "next/link";

interface Props {
    professions: Profession[];
}

export const ProfessionsSkills = ({professions}: Props) => {
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
                        <Link key={profession.id} href={`/professions-skills/${profession.id}`}>
                            <Button style={{flex: 1}}>
                                Detailed  
                            </Button>
                        </Link>
                    </div>
                </Card>
            ))}
        </div>
    )
};