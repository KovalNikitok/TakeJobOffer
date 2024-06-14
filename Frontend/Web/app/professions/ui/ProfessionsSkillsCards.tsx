import Link from "next/link";
import Card from "antd/es/card/Card";

import { CardTitle } from "../../shared/ui/Cardtitle";

interface Props {
    professions: ProfessionWithSlug[];
}

export const ProfessionsSkillsCards = ({professions}: Props) => {
    return (
        <div className="cards__professions">
            {professions.map((profession: ProfessionWithSlug) => (
                <Link key={profession.slug} href={`/professions/${profession.slug}`}>
                    <Card className="card__scaling" key={profession.id} title={
                            <CardTitle name={profession.name} description=""/>
                        }
                        bordered={false}
                    >
                        <p className="card__descr">{profession.description}</p>
                        <div className="card__notify">
                            Смотри требования
                        </div>
                    </Card>
                </Link>
            ))}
        </div>
    );
};