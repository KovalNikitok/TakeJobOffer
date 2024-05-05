import {ProfessionSkillsTable} from "./ProfessionSkillsTable";

interface Props {
    professionSkills: ProfessionSkillWithName[];
}

export const ProfessionSkills = ({professionSkills}: Props) => {
    //let num:number = 1;
    return (
    /*
        <table class="table table-sm table-bordered table-hover ">
            <thead>
                <tr>
                    <th>№</th>
                    <th>Навык</th>
                    <th>Упоминаний</th>
                </tr>
            </thead>
            <tbody>
                let num = 1;
                <tr>
                
                    <td>{{ num++ }}</td>
                    <td>{{ professionSkills.name }}</td>
                    <td>{{ professionSkills.mentionCount }}</td>
                </tr>
            </tbody>
        </table>
    */
        <div className="ps__table">
            { 
                <ProfessionSkillsTable
                    values={professionSkills}
                >
                </ProfessionSkillsTable>
            }
        </div>
    );
};