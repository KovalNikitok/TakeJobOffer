import { ProfessionSkillsTable } from "./ProfessionSkillsTable";

interface Props {
    values: ProfessionSkillWithName[];
    profession: Profession;
}

export const ProfessionSkills = ({values, profession}: Props) => {
    return (
        <div className="ps__table">
            { 
                <ProfessionSkillsTable
                    values={values}
                    profession={profession}
                >
                </ProfessionSkillsTable>
            }
        </div>
    );
};