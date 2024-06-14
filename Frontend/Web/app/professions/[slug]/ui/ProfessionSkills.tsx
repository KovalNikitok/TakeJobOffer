import { ProfessionSkillsTable } from "./ProfessionSkillsTable";

interface Props {
    values: ProfessionSkillWithName[];
}

export const ProfessionSkills = ({values}: Props) => {
    return (
        <div className="ps__table">
            { 
                <ProfessionSkillsTable
                    values={values}
                >
                </ProfessionSkillsTable>
            }
        </div>
    );
};