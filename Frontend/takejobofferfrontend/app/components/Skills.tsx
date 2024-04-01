import Card from "antd/es/card/Card"
import { CardTitle } from "./Cardtitle"
import Button from "antd/es/button/button"

interface Props {
    skills: Skill[];
    handleOpen: (skill: Skill) => void;
    handleDelete: (id: string) => void;
}

export const Skills = ({skills, handleOpen, handleDelete}: Props) => {
    return (
        <div className="cards">
            {skills.map((skill: Skill) => (
                <Card key={skill.id} title={
                        <CardTitle name={skill.name} description=""/>
                    }
                    bordered={false}
                >
                    <div className="card__buttons">
                        <Button 
                            onClick={() => handleOpen(skill)}
                            style={{flex: 1}}
                        >
                            Edit
                        </Button>
                        <Button
                            danger
                            onClick={() => handleDelete(skill.id)}
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