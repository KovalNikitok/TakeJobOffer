import { useEffect, useState } from 'react';
import { Table } from 'antd';
import { AlignType } from 'rc-table/lib/interface';
import CurrentDate from './CurrentDate';

interface Props {
  values: ProfessionSkillWithName[];
  profession: Profession;
}

interface ProfessionSkillColumnProps {
  name: string;
  mentionCount: number;
}

export const ProfessionSkillsTable = ({ values, profession}: Props) => {
    const [professionSkills, setProfessionSkills] = useState(values);
    const [columnsData, setColunmsData] = useState<ProfessionSkillColumnProps[]>([]);
    const [professionValue, setProfession] = useState<Profession>(profession);

    useEffect(() => {
      const SetColumnsProps = () => {
        const columnsProps: ProfessionSkillColumnProps[] = [];
        professionSkills.map((professionSkill) => {
          let col: ProfessionSkillColumnProps = {name: professionSkill.name, mentionCount: professionSkill.mentionCount};
          columnsProps.push(col);
        });
  
        setColunmsData(columnsProps);
      };

      SetColumnsProps();
    }, [professionSkills]);

    

    const columns = [
      {
        title: 'Навык',
        dataIndex: 'name',
        width: 300,
      },
      {
        title: 'Упоминаний',
        dataIndex: 'mentionCount',
        align: 'center' as AlignType,
        width: 30,
        sorter: (firstSkill: ProfessionSkillColumnProps, secondSkill: ProfessionSkillColumnProps) => firstSkill.mentionCount - secondSkill.mentionCount,
      },
    ];
    
    return (
      <div className="ps__table__div">
        <h2 className="ps__table__h2">Требования по навыкам на позицию {profession.description}</h2>
        <p className="ps__table_p">Анализ требований был произведён на момент {CurrentDate()}</p>
        <Table 
          columns={columns} 
          dataSource={columnsData} 
          pagination={{ pageSize: 10, showSizeChanger: false}}
        />
      </div>
    );
}